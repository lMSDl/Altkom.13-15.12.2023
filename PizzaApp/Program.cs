using Models;
using Services.InMemory;
using Services.Interfaces;
using System.Text.Json;

IGenericAsyncService<Pizza> service = new GenericAsyncService<Pizza>();

await service.CreateAsync(new Pizza(true, true));
await service.CreateAsync(new Pizza(true));
await service.CreateAsync(new Pizza(true, true, false, true, true));

Console.WriteLine( JsonSerializer.Serialize(await service.ReadAsync()) );

Pizza pizza = await service.ReadAsync(1);

await service.UpdateAsync(1, pizza);

Console.WriteLine();