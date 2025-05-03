-- Password: private
INSERT INTO dbo.Users (first_name, surname, email, password, role_Id)
VALUES ('John', 'Doe', 'john@example.com', '$2a$11$uOTLVYXpguHJyKIASnfCWuSgF.1mkft/r9PbCLtsnwTG9raAC9bb.', 1)

-- Password: secure
INSERT INTO dbo.Users (first_name, surname, email, password, role_Id)
VALUES ('Jane', 'Smith', 'jane@example.com', '$2a$11$2EzHql79pRvN.Acbc79awu5X3eiD47d8tAq7zDuGCpUHIAIPaTxu2', 2)

-- Password: passwords
INSERT INTO dbo.Users (first_name, surname, email, password, role_Id)
VALUES ('Peter', 'Doe', 'peter@example.com', '$2a$11$/pDaSu9MNVHwHNoEnni2T.1sLUv1jloE0GArT6Tz.p8xpP7GZ5b96', 3)