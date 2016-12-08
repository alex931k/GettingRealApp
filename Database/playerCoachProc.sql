ALTER PROCEDURE AdminLogin (
@username NVARCHAR(25),
@password NVARCHAR(25)
)
AS
BEGIN
      SELECT Count (*) AS result
	  FROM CoachLogin
	  WHERE CoachUserName = @username AND CoachPassword = @password
END


GO
ALTER PROCEDURE UserLogin (
@username NVARCHAR(25),
@password NVARCHAR(25)
)
AS
BEGIN
      SELECT Count (*) AS result
	  FROM PlayerLogin
	  WHERE PlayerUserName = @username AND PlayerPassword = @password
END