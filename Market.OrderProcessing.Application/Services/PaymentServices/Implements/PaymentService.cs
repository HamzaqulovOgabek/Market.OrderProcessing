using AutoMapper;
using E_CommerceProjectDemo.DataAccess.Repositories.OrderRepository;
using E_CommerceProjectDemo.DataAccess.Repositories.PaymentRepository;
using Market.Auth.Application.Services.UserServices;
using Market.OrderProcessing.Application.Exceptions;
using Market.OrderProcessing.Application.Services.PaymentServices;
using Market.OrderProcessing.Domain.Enums;
using Market.OrderProcessing.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProjectDemo.Application.Services.PaymentServices;

public class PaymentService : IPaymentService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public PaymentService(
        IOrderRepository orderRepository, 
        IPaymentRepository paymentRepository,
        IUserService userService,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<PaymentDto> GetAsync(int id)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);

        return _mapper.Map<PaymentDto>(payment);
    }
    public IQueryable<PaymentDto> GetUserPayments(int userId)
    {
        var payments = _paymentRepository.GetAll()
            .Where(p => p.UserId == userId)
            .Select(p => _mapper.Map<PaymentDto>(p));
        return payments;
    }
    public async Task<bool> IsPayedForOrderAsync(int orderId)
    {
        //get payment for this orderId
        var payment = await _paymentRepository.GetAll()
            .FirstOrDefaultAsync(p => p.OrderId == orderId);
        if (payment == null)
            return false;
        //if payment status is completed, it's payed
        return payment.Status == Status.COMPLETED;
    }
    public async Task<int> ProcessPaymentAsync(PaymentCreateDto dto)
    {
        var user = await _userService.GetAsync(dto.UserId);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }

        var order = await _orderRepository.GetByIdAsync(dto.OrderId);
        if (order == null)
            throw new Exception("Order not found.");

        var existingPayment = await _paymentRepository.GetAll().
            FirstOrDefaultAsync(p => p.UserId == dto.UserId &&
                                     p.OrderId == dto.OrderId);
        if (existingPayment != null)
        {
            existingPayment.Amount += dto.Amount;

            existingPayment.Status = GetPaymentStatus(existingPayment.Amount, order);
            await _paymentRepository.UpdateAsync(existingPayment);
            return existingPayment.Id;
        }

        var payment = new Payment
        {
            UserId = dto.UserId,
            OrderId = dto.OrderId,
            Amount = dto.Amount,
            Status = GetPaymentStatus(dto.Amount, order),
        };

        return await _paymentRepository.CreateAsync(payment);
    }
    private static Status GetPaymentStatus(decimal amount, Order? order)
    {
        var paymentStatus = Status.PENDING;
        if (order.TotalPrice == amount)
        {
            paymentStatus = Status.COMPLETED;
        }
        else if (amount < order.TotalPrice)
        {
            paymentStatus = Status.PENDING;
        }

        return paymentStatus;
    }
}