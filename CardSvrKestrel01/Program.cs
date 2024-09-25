var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// 加入 Kestrel 配置 (可選)
//listen http://localhost:5000
/*
builder.WebHost.UseKestrel(options =>
{
    options.ListenLocalhost(5000);  // 監聽 http://localhost:5000
});
*/

// 配置 Kestrel 監聽 HTTPS 的 5000 端口
//listen https://localhost:5000
builder.WebHost.UseKestrel(options =>
{
    options.ListenLocalhost(5000, listenOptions =>
    {
        listenOptions.UseHttps();  // 設置 HTTPS
    });
});




// 加入 CORS 服務，允許特定的 Headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader(); // 確保允許任何 Header，包括 Content-Type
    });
});

// 構建應用程序
var app = builder.Build();

// 啟用 CORS，這必須在 UseRouting 之前
app.UseCors("AllowAllOrigins");

// 必須在 UseRouting 之後
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

// 使用 CORS
app.UseCors("AllowAllOrigins");

app.MapGet("/", () => new { message = "Hello from Kestrel with CORS!" });


app.Run();
