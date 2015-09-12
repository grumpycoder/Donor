-- =============================================
-- Author:		Wade McAvoy (Analysis Factory,LLC)
-- Create date: 2/13/14
-- Description:	Select All or Filtered Events
-- =============================================
CREATE PROCEDURE p_Modify_EventList
	  @Action int, -- 1=Add, 2=Update, 3=Delete
	  @pk_Event int,
	  @User_Added nvarchar(50) = NULL,
	  @EventName nvarchar(25)
AS
BEGIN
	SET NOCOUNT ON;

IF @Action=1 GOTO Modify_Add
ELSE IF @Action=2 GOTO Modify_Update
ELSE IF @Action=3 GOTO Modify_Delete
ELSE GOTO Modify_End

DECLARE @Exists bit

--TESTING
-- Add New Record
Modify_Add:
	IF EXISTS(SELECT pk_Event FROM EventList WHERE EventName=@EventName)
		RETURN 1
	ELSE
	BEGIN
		--INSERT INTO EventList (Date_Added,User_Added,EventName)
		SELECT * FROM EventList
	END
GOTO Modify_End

-- Update Existing Record
Modify_Update:
	SELECT * FROM EventList
GOTO Modify_End

-- Delete Record
Modify_Delete:
	DELETE EventList WHERE pk_Event=@pk_Event

Modify_End:

END
GO
