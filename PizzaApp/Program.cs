using Models;
using Services.InMemory;
using Services.Interfaces;
using System.Text.Json;

IGenericService<Pizza> service = new GenericService<Pizza>();

service.Create(new Pizza(true, true));
service.Create(new Pizza(true));
service.Create(new Pizza(true, true, false, true, true));

Console.WriteLine( JsonSerializer.Serialize(service.Read()) );