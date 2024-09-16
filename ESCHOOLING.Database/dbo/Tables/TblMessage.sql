CREATE TABLE [dbo].[TblMessage](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FromUserId] [int] NOT NULL,
	[ToUserId] [int] NOT NULL,
	[TimeSent] [datetime] NOT NULL,
	[HasRecieverRed] [bit] NOT NULL,
	[MessageText] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]