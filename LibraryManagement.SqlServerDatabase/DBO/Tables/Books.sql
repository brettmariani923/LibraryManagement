CREATE TABLE [dbo].[Books]
(
	BookID INT CONSTRAINT PK_Books PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(150) NOT NULL,
    PublishedYear NVARCHAR(10),
    Summary NVARCHAR(1500),
    CONSTRAINT CHK_Book_Title CHECK (LEN(Title) > 0), 
    CONSTRAINT UQ_Book_Title UNIQUE(Title)
);
