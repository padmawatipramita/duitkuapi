CREATE DATABASE DuitKu

USE DuitKu

CREATE TABLE msUser
(
	UserID INT IDENTITY(1,1) PRIMARY KEY,
	UserName VARCHAR(255) NOT NULL,
	UserEmail VARCHAR(255) CHECK(UserEmail LIKE '%@%.%'),
	UserPassword VARCHAR(255),
	UserBalance INT NOT NULL
)

CREATE TABLE trTransaction
(
	TransactionID INTEGER IDENTITY (1,1) PRIMARY KEY,
	Balance INT NOT NULL,
	Date DATETIME NOT NULL,
	Notes VARCHAR(255) NOT NULL,
	TransactionType VARCHAR(255),
	UserID INT,
	FOREIGN KEY(UserID) REFERENCES msUser(UserID)
)

INSERT INTO msUser VALUES('Mita', 'mita@gmail.com', 'halo', 100000)

SELECT userbalance + (select sum(balance) from trTransaction where userid = 1 and TransactionType = 'Income') - (select sum(balance) from trTransaction where userid = 1 and TransactionType = 'Expense') as 'FinalBalance' , *  FROM msUser
SELECT * FROM trTransaction

SELECT GETDATE()

SELECT * FROM MsUser

DELETE FROM msUser WHERE UserId = 7