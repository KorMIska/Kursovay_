-- Вставка тестовых данных в таблицу "University"
INSERT INTO University (Name, Address) VALUES ('Университет Пример', 'ул. Примерная, 123');

-- Вставка тестовых данных в таблицу "Applicant"
INSERT INTO Applicant (ID, Full_Name, Phone) VALUES (1, 'Иванов Иван Иванович', '123-456-7890');
INSERT INTO Applicant (ID, Full_Name, Phone) VALUES (2, 'Петрова Мария Сергеевна', '987-654-3210');

-- Вставка тестовых данных в таблицу "Document"
INSERT INTO Document (ID, Document_Type, Document_Code) VALUES (1, 'Паспорт', 'ABC123');
INSERT INTO Document (ID, Document_Type, Document_Code) VALUES (2, 'Удостоверение личности', 'DEF456');

-- Вставка тестовых данных в таблицу "Specialty"
INSERT INTO Specialty (ID, Name, Seats) VALUES (1, 'Информатика', 100);
INSERT INTO Specialty (ID, Name, Seats) VALUES (2, 'Экономика', 50);

-- Вставка тестовых данных в таблицу "Dormitory"
INSERT INTO Dormitory (ID, Status) VALUES (1, 'Доступно');
INSERT INTO Dormitory (ID, Status) VALUES (2, 'Занято');

-- Вставка тестовых данных в таблицу "Application"
INSERT INTO Application (ID, Status) VALUES (1, 'Ожидает рассмотрения');
INSERT INTO Application (ID, Status) VALUES (2, 'Одобрено');

-- Вставка тестовых данных в таблицу "Scores"
INSERT INTO Scores (ID, Subject, Score) VALUES (1, 'Математика', 90);
INSERT INTO Scores (ID, Subject, Score) VALUES (2, 'Английский', 85);

-- Вставка тестовых данных в таблицу "Commission"
INSERT INTO Commission (Chairman_Name, Meeting_Date) VALUES ('Иванов Иван Иванович', '2022-01-01');