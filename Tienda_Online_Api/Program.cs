using AutoMapper;
using Controlador;
using Controlador.Interfazes;
using Controlador.Usuarios;
using Modelo.DTOS.CREATE;
using Modelo.DTOS.CREATE_DTO;
using Tienda_Online_Api;
using Tienda_Online_Api.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TiendaOnlineContext>();

// Configuración de la inyección de dependencias
builder.Services.AddScoped<IMetodosBasicos<CrearUsuariosDTO>, UsuariosControlador>();
builder.Services.AddScoped<IMetodosBasicos<CrearProductosDTO>, ProductosControlador>();
builder.Services.AddScoped<PedidoControlador>();
builder.Services.AddScoped<HistorialControlador>();
builder.Services.AddScoped<CarritoControlador>();

// Configuración de AutoMapper
var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Ingresar datos iniciales

// Asegurarse de que la base de datos esté creada
using (var context = new TiendaOnlineContext())
{
    context.Database.EnsureCreated();
}

var app = builder.Build();

//este pedazo de aqui inserta 10 datos de cada entidad en caso de no querer estar jodiendo ingresando datos

/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TiendaOnlineContext>();

    // Crear la instancia de IngresarData_Iniciando y llamar al método
    var ingresarData = new IngresarData_Iniciando(context);
    ingresarData.Ingresar_Data_Para_No_Joder_Con_Datos();
}
*/

// Configuración del pipeline de solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
