CREATE INDEX [INDEX_DonorEventList_Event_TicketsMailedDate]
	ON [dbo].[DonorEventList]
	([fk_Event] ASC, [TicketsMailed_Date] ASC)
