CREATE TABLE [dbo].[TblChatMessage]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [FromUserId] BIGINT NOT NULL, 
    [ToUserId] BIGINT NOT NULL, 
    [TimeSent] DATETIME NOT NULL, 
    [HasRecieverRed] BIT NOT NULL DEFAULT 0, 
    [MessageText] NVARCHAR(MAX) NOT NULL
)
