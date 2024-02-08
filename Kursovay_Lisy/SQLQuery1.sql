-- Creating table "University"
CREATE TABLE University (
    Name NVARCHAR(255),
    Address NVARCHAR(255)
);

-- Creating table "Applicant"
CREATE TABLE Applicant (
    ID INT PRIMARY KEY,
    Full_Name NVARCHAR(255),
    Phone NVARCHAR(20)
);

-- Creating table "Document"
CREATE TABLE Document (
    ID INT PRIMARY KEY,
    Document_Type NVARCHAR(255),
    Document_Code NVARCHAR(255)
);

-- Creating table "Specialty"
CREATE TABLE Specialty (
    ID INT PRIMARY KEY,
    Name NVARCHAR(255),
    Seats INT
);

-- Creating table "Dormitory"
CREATE TABLE Dormitory (
    ID INT PRIMARY KEY,
    Status NVARCHAR(255)
);

-- Creating table "Application"
CREATE TABLE Application (
    ID INT PRIMARY KEY,
    Status NVARCHAR(255)
);

-- Creating table "Scores"
CREATE TABLE Scores (
    ID INT PRIMARY KEY,
    Subject NVARCHAR(255),
    Score INT
);

-- Creating table "Commission"
CREATE TABLE Commission (
    Chairman_Name NVARCHAR(255),
    Meeting_Date DATE
);