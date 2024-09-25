var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// �[�J Kestrel �t�m (�i��)
//listen http://localhost:5000
/*
builder.WebHost.UseKestrel(options =>
{
    options.ListenLocalhost(5000);  // ��ť http://localhost:5000
});
*/

// �t�m Kestrel ��ť HTTPS �� 5000 �ݤf
//listen https://localhost:5000
builder.WebHost.UseKestrel(options =>
{
    options.ListenLocalhost(5000, listenOptions =>
    {
        listenOptions.UseHttps();  // �]�m HTTPS
    });
});




// �[�J CORS �A�ȡA���\�S�w�� Headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader(); // �T�O���\���� Header�A�]�A Content-Type
    });
});

// �c�����ε{��
var app = builder.Build();

// �ҥ� CORS�A�o�����b UseRouting ���e
app.UseCors("AllowAllOrigins");

// �����b UseRouting ����
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// �ϥ� CORS
app.UseCors("AllowAllOrigins");

app.MapGet("/", () => new { message = "Hello from Kestrel with CORS!" });


app.Run();
