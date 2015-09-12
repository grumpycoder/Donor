-- =============================================
-- Author:		Wade McAvoy (Analysis Factory,LLC)
-- Create date: 12/15/14
-- Description:	Create Currency Value
-- =============================================
CREATE FUNCTION [dbo].[CurValue] 
(
	@CValue varchar(100)
)
RETURNS money
AS
BEGIN
	
	SET @CValue = RTRIM(LTRIM(@CValue))
	IF(ISNUMERIC(@CValue) = 1)
	BEGIN
		RETURN @CValue
	END
	ELSE
	BEGIN
		RETURN NULL
	END
	
	RETURN @CValue
END