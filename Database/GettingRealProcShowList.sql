USE [EJL87_DB]
GO
/****** Object:  StoredProcedure [db_owner].[ShowPlayerInfo]    Script Date: 12/01/2016 14:42:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [db_owner].[ShowPlayerInfo]
AS
BEGIN
       SELECT PlayerFirstName, PlayerLastName, PlayerEmail, PlayerPhone, PlayerLogin.PlayerUserName
	   FROM PlayerList
	   INNER JOIN PlayerLogin
	   ON PlayerList.PlayerID=PlayerLogin.PlayerID;
	   
END


