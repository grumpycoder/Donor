-- =============================================
-- Author:		Wade McAvoy (Analysis Factory,LLC)
-- Create date: 2/13/14
-- Description:	Select All or Filtered Events
-- =============================================
CREATE PROCEDURE [dbo].[p_SelectEventList]

AS
BEGIN

SET NOCOUNT ON;

SELECT *
FROM EventList

--SELECT 'Wade', 'McAvoy'

END
