USE [AccountDb]

-- ExpenseTypes
SET IDENTITY_INSERT [dbo].[ExpenseTypes] ON

INSERT INTO [dbo].[ExpenseTypes]
		([ExpenseTypeId], [ExpenseTypeName])
VALUES
		(1, 'Vendor'),
		(2, 'Commission Agent')

SET IDENTITY_INSERT [dbo].[ExpenseTypes] OFF

