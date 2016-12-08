ALTER PROCEDURE UserLogin (
@username NVARCHAR(25),
@password NVARCHAR(25)
)
AS
BEGIN
      SELECT PlayerUserName, PlayerPassword AS result
	  FROM PlayerList
	  WHERE PlayerUserName = @username AND PlayerPassword = @password
END