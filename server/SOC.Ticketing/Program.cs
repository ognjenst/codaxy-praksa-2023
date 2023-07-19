using SOC.Ticketing;

TicketingService ticketingService = new TicketingService();
ticketingService.DisplayConnectionStatus();

ticketingService.PublishMessage();
ticketingService.ReceiveMessages();
ticketingService.Subscribe();