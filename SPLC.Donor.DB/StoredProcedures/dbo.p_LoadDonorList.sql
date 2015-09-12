-- =============================================
-- Author:		Wade McAvoy (Analysis Factory,LLC)
-- Create date: 2/13/14
-- Description:	Select All or Filtered Events
-- =============================================
CREATE PROCEDURE p_LoadDonorList
	@pk_Event int,
	@TicketCount int,
	@UserAdded nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

DECLARE @pk_Donor nvarchar(15)

DECLARE Donor_Cursor CURSOR FOR 
SELECT pk_DonorList FROM DonorListStage

OPEN Donor_Cursor
FETCH NEXT FROM Donor_Cursor
INTO @pk_Donor

WHILE @@FETCH_STATUS = 0
BEGIN

	/*********************************************************
	*	Load Donor Information into System
	*********************************************************/
	IF EXISTS(SELECT pk_DonorList FROM DonorList WHERE pk_DonorList=@pk_Donor)
	BEGIN
		DELETE DonorList WHERE pk_DonorList=@pk_Donor		
	END
	
	INSERT INTO DonorList (pk_DonorList, DonorType, AccountType, KeyName, AccountID, InsideSal, OutSideSal, HHOutsideSal, AccountName, AddressLine1, 
							AddressLine2, AddressLine3, AddressLine4, AddressLine5, City, State, PostCode, CountryIDTrans, StateDescription, 
							EmailAddress, PhoneNumber, SPLCLeadCouncil, MembershipYear, YearsSince, HPC, LastPaymentDate, LastPaymentAmount, SourceCode)
	(SELECT pk_DonorList, DonorType, AccountType, KeyName, AccountID, InsideSal, OutSideSal, HHOutsideSal, AccountName, AddressLine1, 
							AddressLine2, AddressLine3, AddressLine4, AddressLine5, City, State, PostCode, CountryIDTrans, StateDescription, 
							EmailAddress, PhoneNumber, SPLCLeadCouncil, MembershipYear, YearsSince, HPC, LastPaymentDate, LastPaymentAmount, SourceCode
	 FROM DonorListStage WHERE pk_DonorList=@pk_Donor) 
	/*********************************************************/
	
	/*********************************************************
	*	If Record is not attached to the event, add
	*********************************************************/
	IF NOT EXISTS(SELECT pk_DonorEventList FROM DonorEventList WHERE fk_Event=@pk_Event AND fk_DonorList=@pk_Donor)
	BEGIN
		INSERT INTO DonorEventList (fk_Event,fk_DonorList,Date_Added,User_Added,TicketsRequested)
		VALUES (@pk_Event,@pk_Donor,GETDATE(),@UserAdded,0)
	END
	/*********************************************************/

FETCH NEXT FROM Donor_Cursor 
INTO @pk_Donor

END
CLOSE Donor_Cursor
DEALLOCATE Donor_Cursor

DELETE DonorListStage

END
GO
