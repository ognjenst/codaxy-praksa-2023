using SOC.Ticketing.Generated;
using SOC.Ticketing.Services;


TicketingService ticketingService = new TicketingService();
InputCreateTask inputCreateTask = new InputCreateTask();

/*inputCreateTask.Title = "task";
inputCreateTask.Description = "desc";

var createOutput = await ticketingService.CreateTaskAsync(inputCreateTask, "~8106216");
Console.WriteLine("id " + createOutput.Id);*/

/*var getOutput = await ticketingService.GetTaskAsync("~4214816");
Console.WriteLine(getOutput.Id);

await ticketingService.DeleteTaskAsync("~4214816");
*/

InputUpdateTask inputUpdateTask = new InputUpdateTask();
inputUpdateTask.Title = "updatedTitle";
await ticketingService.UpdateTaskAsync("~4116624", inputUpdateTask);