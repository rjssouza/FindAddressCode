using Api.Filter;
using Domain.Mapper;
using Infrastructure.Extension;
using Integration.HttpIntegration;
using Integration.Interface;
using Service.Interface;
using Service.UseCase;
using Service.Validation.Interface;
using Service.Validation.Rules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddControllerServices(typeof(ExceptionFilter));

// Add and configure api swagger
builder.AddSwagger();

// Dependency injection of services
builder.Services.RegisterAllTypes(typeof(IUseCase<,>), new[] { typeof(BaseUseCase).Assembly });
builder.Services.RegisterAllTypes(typeof(IUseCase<>), new[] { typeof(BaseUseCase).Assembly });
builder.Services.RegisterAllTypes(typeof(IUseCaseValidation<>), new[] { typeof(BaseValidation<>).Assembly });
builder.Services.AddScoped<IPostCodeIntegration, PostCodeIntegration>();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(PostCodeProfile).Assembly);

var app = builder.Build();

app.UseCors("AllowOrigin"); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.Run();
