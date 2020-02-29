create database SignalRChat

-----

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Phone] [varchar] (12) NOT NULL,
	[Age] [smallint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Icon] [varchar] (255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

------

CREATE TABLE [dbo].[SignInCodes](
	[CodeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Code] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SignInCodes]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

---------

CREATE TABLE [dbo].[Messages](
	[UsersChatId] [bigint] IDENTITY(1,1) NOT NULL,
	[MsgFromUserId] [int] NOT NULL,
	[MsgToUserId] [int] NOT NULL,
	[Msg] [varchar](255) NOT NULL,
	[SendDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsersChatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_MsgFromUser] FOREIGN KEY([MsgFromUserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_MsgFromUser]
GO

ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_MsgToUser] FOREIGN KEY([MsgToUserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_MsgToUser]
GO










