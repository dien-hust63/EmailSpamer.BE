using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PaymentGateway.BL;
using PaymentGateway.Interface;
using WebBanHang.Api.Middleware;
using WebBanHang.BL.BL;
using WebBanHang.Common.AzureStorage;
using WebBanHang.Common.DBHelper;
using WebBanHang.Common.Interfaces.Base;
using WebBanHang.Common.Interfaces.BL;
using WebBanHang.Common.Interfaces.DL;
using WebBanHang.Common.ServiceCollection;
using WebBanHang.Common.Services;
using WebBanHang.DL.BaseDL;
using WebBanHang.DL.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<CoreServicesCollection>();
// DL DI
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IRoleBL, RoleBL>();
builder.Services.AddScoped<IModuleBL, ModuleBL>();
builder.Services.AddScoped<IProductBL, ProductBL>();
builder.Services.AddScoped<IBranchBL, BranchBL>();
builder.Services.AddScoped<IAuthenticationBL, AuthenticationBL>();
builder.Services.AddScoped<IPaymentBL, PaymentBL>();
builder.Services.AddScoped<IAzureStorageBL, AzureStorageBL>();
builder.Services.AddScoped<IDBHelper, DBHelper>();
builder.Services.AddScoped<IMailDL, MailDL>();
builder.Services.AddScoped<IOrderBL, OrderBL>();

builder.Services.AddScoped<ISenderBL, SenderBL>();
builder.Services.AddScoped<IReceiverBL, ReceiverBL>();
builder.Services.AddScoped<ICampaignBL, CampaignBL>();
//BL DI
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IModuleDL, ModuleDL>();
builder.Services.AddScoped<IRoleDL, RoleDL>();
builder.Services.AddScoped<IBranchDL, BranchDL>();
builder.Services.AddScoped<IAuthenticationDL, AuthenticationDL>();
builder.Services.AddScoped<IMailBL, MailBL>();
builder.Services.AddScoped<IProductDL, ProductDL>();
builder.Services.AddScoped<IOrderDL, OrderDL>();

builder.Services.AddScoped<ISenderDL, SenderDL>();
builder.Services.AddScoped<IReceiverDL, ReceiverDL>();
builder.Services.AddScoped<ICampaignDL, CampaignDL>();
// swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebBanHang", Version = "v1" });
});

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
