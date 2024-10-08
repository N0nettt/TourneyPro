USE [master]
GO
/****** Object:  Database [TourneyProApp]    Script Date: 8/22/2024 6:37:48 PM ******/
CREATE DATABASE [TourneyProApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TourneyProApp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TourneyProApp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TourneyProApp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TourneyProApp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TourneyProApp] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TourneyProApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TourneyProApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TourneyProApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TourneyProApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TourneyProApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TourneyProApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [TourneyProApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TourneyProApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TourneyProApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TourneyProApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TourneyProApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TourneyProApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TourneyProApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TourneyProApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TourneyProApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TourneyProApp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TourneyProApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TourneyProApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TourneyProApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TourneyProApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TourneyProApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TourneyProApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TourneyProApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TourneyProApp] SET RECOVERY FULL 
GO
ALTER DATABASE [TourneyProApp] SET  MULTI_USER 
GO
ALTER DATABASE [TourneyProApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TourneyProApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TourneyProApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TourneyProApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TourneyProApp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TourneyProApp] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TourneyProApp', N'ON'
GO
ALTER DATABASE [TourneyProApp] SET QUERY_STORE = OFF
GO
USE [TourneyProApp]
GO
/****** Object:  Table [dbo].[Brackets]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brackets](
	[BracketID] [int] IDENTITY(1,1) NOT NULL,
	[TournamentID] [int] NOT NULL,
 CONSTRAINT [PK_Brackets_1] PRIMARY KEY CLUSTERED 
(
	[BracketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Matches]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matches](
	[MatchID] [int] NOT NULL,
	[FirstParticipantID] [int] NULL,
	[SecondParticipantID] [int] NULL,
	[WinnerID] [int] NULL,
	[NextMatch] [int] NOT NULL,
	[RoundID] [int] NOT NULL,
	[TournamentID] [int] NOT NULL,
 CONSTRAINT [PK__Matches__4218C83710366031] PRIMARY KEY CLUSTERED 
(
	[MatchID] ASC,
	[TournamentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Participants]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Participants](
	[ParticipantID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Participant] PRIMARY KEY CLUSTERED 
(
	[ParticipantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prizes]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prizes](
	[placeNumber] [int] NOT NULL,
	[prizeAmount] [float] NOT NULL,
	[prizePercentage] [float] NOT NULL,
	[TournamentID] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Prizes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] NOT NULL,
	[RoleName] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rounds]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rounds](
	[RoundID] [int] IDENTITY(1,1) NOT NULL,
	[BracketID] [int] NOT NULL,
	[RoundNumber] [int] NOT NULL,
 CONSTRAINT [PK_Rounds] PRIMARY KEY CLUSTERED 
(
	[RoundID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TournamentParticipants]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TournamentParticipants](
	[TournamentID] [int] NOT NULL,
	[ParticipantID] [int] NOT NULL,
 CONSTRAINT [PK_TournamentParticipants] PRIMARY KEY CLUSTERED 
(
	[TournamentID] ASC,
	[ParticipantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tournaments]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tournaments](
	[Name] [nvarchar](50) NOT NULL,
	[Date] [date] NOT NULL,
	[NumberOfParticipant] [int] NOT NULL,
	[Payouts] [int] NOT NULL,
	[Fee] [float] NOT NULL,
	[TournamentID] [int] IDENTITY(1,1) NOT NULL,
	[Organizer] [nvarchar](50) NOT NULL,
	[Winner] [int] NULL,
 CONSTRAINT [PK_Tournaments] PRIMARY KEY CLUSTERED 
(
	[TournamentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Brackets]    Script Date: 8/22/2024 6:37:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Brackets] ON [dbo].[Brackets]
(
	[TournamentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users]    Script Date: 8/22/2024 6:37:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users] ON [dbo].[Users]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_RoleID]  DEFAULT ((1)) FOR [RoleID]
GO
ALTER TABLE [dbo].[Brackets]  WITH CHECK ADD  CONSTRAINT [FK_Brackets_Tournaments] FOREIGN KEY([TournamentID])
REFERENCES [dbo].[Tournaments] ([TournamentID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Brackets] CHECK CONSTRAINT [FK_Brackets_Tournaments]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK__Matches__FirstPa__778AC167] FOREIGN KEY([FirstParticipantID])
REFERENCES [dbo].[Participants] ([ParticipantID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK__Matches__FirstPa__778AC167]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK__Matches__SecondP__787EE5A0] FOREIGN KEY([SecondParticipantID])
REFERENCES [dbo].[Participants] ([ParticipantID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK__Matches__SecondP__787EE5A0]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK__Matches__WinnerI__797309D9] FOREIGN KEY([WinnerID])
REFERENCES [dbo].[Participants] ([ParticipantID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK__Matches__WinnerI__797309D9]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Rounds] FOREIGN KEY([RoundID])
REFERENCES [dbo].[Rounds] ([RoundID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Rounds]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Tournaments] FOREIGN KEY([TournamentID])
REFERENCES [dbo].[Tournaments] ([TournamentID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Tournaments]
GO
ALTER TABLE [dbo].[Prizes]  WITH CHECK ADD  CONSTRAINT [FK_Prizes_Tournaments] FOREIGN KEY([TournamentID])
REFERENCES [dbo].[Tournaments] ([TournamentID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prizes] CHECK CONSTRAINT [FK_Prizes_Tournaments]
GO
ALTER TABLE [dbo].[Rounds]  WITH CHECK ADD  CONSTRAINT [FK_Rounds_Brackets] FOREIGN KEY([BracketID])
REFERENCES [dbo].[Brackets] ([BracketID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rounds] CHECK CONSTRAINT [FK_Rounds_Brackets]
GO
ALTER TABLE [dbo].[TournamentParticipants]  WITH CHECK ADD  CONSTRAINT [FK_TournamentParticipants_Participants] FOREIGN KEY([ParticipantID])
REFERENCES [dbo].[Participants] ([ParticipantID])
GO
ALTER TABLE [dbo].[TournamentParticipants] CHECK CONSTRAINT [FK_TournamentParticipants_Participants]
GO
ALTER TABLE [dbo].[TournamentParticipants]  WITH CHECK ADD  CONSTRAINT [FK_TournamentParticipants_Tournaments] FOREIGN KEY([TournamentID])
REFERENCES [dbo].[Tournaments] ([TournamentID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TournamentParticipants] CHECK CONSTRAINT [FK_TournamentParticipants_Tournaments]
GO
ALTER TABLE [dbo].[Tournaments]  WITH CHECK ADD  CONSTRAINT [FK_Tournaments_Participants] FOREIGN KEY([Winner])
REFERENCES [dbo].[Participants] ([ParticipantID])
GO
ALTER TABLE [dbo].[Tournaments] CHECK CONSTRAINT [FK_Tournaments_Participants]
GO
ALTER TABLE [dbo].[Tournaments]  WITH CHECK ADD  CONSTRAINT [FK_Tournaments_Users] FOREIGN KEY([Organizer])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Tournaments] CHECK CONSTRAINT [FK_Tournaments_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
/****** Object:  StoredProcedure [dbo].[spCreateBracket]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateBracket]
    @TournamentID int,
    @BracketID int OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[Brackets] (TournamentID)
    VALUES (@TournamentID);

    SET @BracketID = SCOPE_IDENTITY();
END

GO
/****** Object:  StoredProcedure [dbo].[spCreateMatch]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateMatch]
    @MatchID int,
    @NextMatch int,
    @RoundID int,
	@TournamentID int
AS
BEGIN
    INSERT INTO Matches(MatchID, RoundID, NextMatch, TournamentID) VALUES  (@MatchID, @RoundID, @NextMatch, @TournamentID)
    
    
END
GO
/****** Object:  StoredProcedure [dbo].[spCreateParticipant]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateParticipant]
    @Name nvarchar(50),
    @Email varchar(50),
    @ParticipantID int OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[Participants] (Name, Email)
    VALUES (@Name, @Email);

    SET @ParticipantID = SCOPE_IDENTITY();
END

GO
/****** Object:  StoredProcedure [dbo].[spCreatePrize]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreatePrize]
    @placeNumber int,
    @prizeAmount float,
    @prizePercentage float,
    @TournamentID int,
    @id int OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[Prizes] (placeNumber, prizeAmount, prizePercentage, TournamentID)
    VALUES (@placeNumber, @prizeAmount, @prizePercentage, @TournamentID);

    SET @id = SCOPE_IDENTITY();
END

GO
/****** Object:  StoredProcedure [dbo].[spCreateRole]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateRole]
    @Role varchar(15),
    @RoleID int OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[Roles] (Role)
    VALUES (@Role);

    SET @RoleID = SCOPE_IDENTITY();
END

GO
/****** Object:  StoredProcedure [dbo].[spCreateRound]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateRound]
    @BracketID int,
    @RoundNumber int,
    @RoundID int OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[Rounds] (BracketID, RoundNumber)
    VALUES (@BracketID, @RoundNumber);

    SET @RoundID = SCOPE_IDENTITY();
END

GO
/****** Object:  StoredProcedure [dbo].[spCreateTournament]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateTournament]
	@Name nvarchar(50),
	@Date datetime,
	@NumberOfParticipant int,
	@Payouts int,
	@Fee float,
	@Organizer nvarchar(50),
	@TournamentID int = 0 output
AS
BEGIN
	
	SET NOCOUNT ON;
	
	INSERT INTO dbo.Tournaments(Name, Date, NumberOfParticipant, Payouts, Fee, Organizer) values 
	(@Name, @Date, @NumberOfParticipant, @Payouts, @Fee, @Organizer)
    
	SELECT @TournamentID = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[spCreateTournamentParticipant]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateTournamentParticipant]
    @TournamentID int,
    @ParticipantID int
AS
BEGIN
    INSERT INTO [dbo].[TournamentParticipants] (TournamentID, ParticipantID)
    VALUES (@TournamentID, @ParticipantID);
END

GO
/****** Object:  StoredProcedure [dbo].[spRegisterUser]    Script Date: 8/22/2024 6:37:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spRegisterUser]
	@Username nvarchar(50),
	@Password nvarchar(50),
	@Email varchar(50),
	@RoleID int
AS
BEGIN	
	SET NOCOUNT ON;
	
	INSERT INTO dbo.Users(Username, Password, Email, RoleID) values 
	(@Username, @Password, @Email, @RoleID)   
END
GO
USE [master]
GO
ALTER DATABASE [TourneyProApp] SET  READ_WRITE 
GO
