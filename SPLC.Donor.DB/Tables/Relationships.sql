ALTER TABLE [dbo].[DonorEventList]  WITH CHECK ADD  CONSTRAINT [FK_DonorEventList_DonorList] FOREIGN KEY([fk_DonorList])
REFERENCES [dbo].[DonorList] ([pk_DonorList])
GO

ALTER TABLE [dbo].[DonorEventList] CHECK CONSTRAINT [FK_DonorEventList_DonorList]
GO

ALTER TABLE [dbo].[DonorEventList]  WITH CHECK ADD  CONSTRAINT [FK_DonorEventList_EventList] FOREIGN KEY([fk_Event])
REFERENCES [dbo].[EventList] ([pk_Event])
GO

ALTER TABLE [dbo].[DonorEventList] CHECK CONSTRAINT [FK_DonorEventList_EventList]
GO