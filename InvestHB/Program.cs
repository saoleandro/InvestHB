using FluentValidation.Results;
using InvestHB.Domain.AutoMapper;
using InvestHB.Domain.Commands;
using InvestHB.Domain.Commands.Handler;
using InvestHB.Domain.Interfaces.Repository;
using InvestHB.Domain.Interfaces.Services;
using InvestHB.Domain.Services;
using InvestHB.Repository;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

//ou direto no appsettings.json o kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(configuration.GetValue<int>("PortaHttp"));
    options.ListenAnyIP(configuration.GetValue<int>("PortaHttps"), configure => configure.UseHttps());

});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "InvestHB", Version = "v1" });
});

builder.Services
    .AddTransient<IInstrumentInfoRepository, InstrumentInfoRepository>()
    .AddTransient<IOrderRepository, OrderRepository>()
    .AddTransient<IOrderService, OrderService>()
    .AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddAutoMapper(typeof(OrderMapperProfile));

builder.Services.AddScoped<IRequestHandler<CreateOrderCommand, Tuple<ValidationResult, int>>, OrderCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateOrderCommand, Tuple<ValidationResult, int>>, OrderCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteOrderCommand, Tuple<ValidationResult, int>>, OrderCommandHandler>();

var app = builder.Build();

IConfiguration Configuration = app.Configuration;

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvestHB v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();