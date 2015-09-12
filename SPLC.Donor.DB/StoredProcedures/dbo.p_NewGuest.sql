-- =============================================
-- Author:		Wade McAvoy (Analysis Factory,LLC)
-- Create date: 3/11/14
-- Description:	Get new Guest
-- =============================================
CREATE PROCEDURE p_NewGuest
AS
BEGIN
	SET NOCOUNT ON;

DECLARE @pNEWID varchar(15)
DECLARE @pID int

SET @pNEWID = (SELECT MAX(pk_DonorList) FROM DonorList WHERE pk_DonorList >= 99990000000)

-- Get new ID for guest
IF(@pNEWID IS NULL)
BEGIN
	SET @pNEWID = '99990000000'
	SET @pID = CAST(SUBSTRING(@pNEWID,4,8) AS INT)
END
ELSE
BEGIN
	SET @pNEWID = (SELECT MAX(pk_DonorList) FROM DonorList)
	SET @pID = CAST(SUBSTRING(@pNEWID,4,8) AS INT) + 1
END

-- Add guest to Donor List table
IF NOT EXISTS(SELECT pk_DonorList from DonorList WHERE pk_DonorList='999' + CAST(@pID AS varchar(8)))
BEGIN
INSERT INTO DonorList (pk_DonorList,DonorType,AccountType)
VALUES ('999' + CAST(@pID AS varchar(8)),'Guest','Guest')
END

SELECT '999' + CAST(@pID AS varchar(8))

END
