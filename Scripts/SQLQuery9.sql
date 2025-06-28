-- إدخال بيانات في جدول Faculty
INSERT INTO Faculty 
VALUES  
    ('CS', 'Computer Science'),
    ('CS', 'Engineering');


-- إدخال بيانات في جدول FacultyYear
INSERT INTO FacultyYear    
VALUES
    ('1-ST',1),
    ('2-ND',1),
    ('3-RD',1),
    ('4-TH',1);

-- إدخال بيانات في جدول FacultyYearSemister
INSERT INTO FacultyYearSemister
VALUES 
    ('S1', 'Sem-1', 1),
    ('S2', 'Sem-2', 1),
	('S1', 'Sem-1', 2),
    ('S2', 'Sem-2', 2),
	('S1', 'Sem-1', 3),
    ('S2', 'Sem-2', 3),
	('S1', 'Sem-1', 4),
    ('S2', 'Sem-2', 4);

-- Insert data into Students table 
 INSERT INTO Students  
VALUES 
    ('S001', N'Ahmed Ali', 'Ahmed Ali', 'Ahmed_Ali.com', 'image1.jpg', '0123456789', NULL,  1),
    ('S002', N'Sara Mohamed', 'Sara Mohamed', 'Sara_Mohamed.com', 'image2.jpg', '0987654321', NULL,   1),
    ('S003', N'Mohamed Samir', 'Mohamed Samir', 'Mohamed_Samir.com', 'image3.jpg', '0111222333', NULL,   1),
    ('S004', N'Laila Hassan', 'Laila Hassan', 'Laila_Hassan.com', 'image4.jpg', '0223344556', NULL,   1),
    ('S005', N'Karim Youssef', 'Karim Youssef', 'Karim_Youssef.com', 'image5.jpg', '0334455667', NULL,  1),
    ('S006', N'Huda Abdullah', 'Huda Abdullah', 'Huda_Abdullah.com', 'image6.jpg', '0445566778', NULL,  1),
    ('S007', N'Mazen Ibrahim', 'Mazen Ibrahim', 'Mazen_Ibrahim.com', 'image7.jpg', '0556677889', NULL,  1),
    ('S008', N'Yasmin Salah', 'Yasmin Salah', 'Yasmin_Salah.com', 'image8.jpg', '0667788990', NULL,   1),
    ('S009', N'Nour El Din Mahmoud', 'Nour El Din Mahmoud', 'Nour_Mahmoud.com', 'image9.jpg', '0778899001', NULL,  1),
    ('S010', N'Rana Khaled', 'Rana Khaled', 'Rana_Khaled.com', 'image10.jpg', '0889900112', NULL,  1);


-- Insert data into Doctors table
INSERT INTO Doctors  
VALUES
    ('D001', N'Dr. Mohamed Youssef', 'Dr. Mohamed Youssef', 'Mohamed_Youssef.com', 'pass123', '01123456789',1),
    ('D002', N'Dr. Nada Mostafa', 'Dr. Nada Mostafa', 'Nada_Mostafa.com', 'pass456', '01098765432',1),
    ('D003', N'Dr. Sami Ibrahim', 'Dr. Sami Ibrahim', 'Sami_Ibrahim.com', 'pass789', '01234567890',1),
    ('D004', N'Dr. Hala Fawzy', 'Dr. Hala Fawzy', 'Hala_Fawzy.com', 'pass321', '01987654321',1),
    ('D005', N'Dr. Amr Zakaria', 'Dr. Amr Zakaria', 'Amr_Zakaria.com', 'pass654', '01876543210',1),
    ('D006', N'Dr. Reem Nabil', 'Dr. Reem Nabil', 'Reem_Nabil.com', 'pass111', '01011223344',1),
    ('D007', N'Dr. Karim Abdallah', 'Dr. Karim Abdallah', 'Karim_Abdallah.com', 'pass222', '01022334455',1),
    ('D008', N'Dr. Yasmin Hussein', 'Dr. Yasmin Hussein', 'Yasmin_Hussein.com', 'pass333', '01033445566',1),
    ('D009', N'Dr. Hossam Kamal', 'Dr. Hossam Kamal', 'Hossam_Kamal.com', 'pass444', '01044556677',1),
    ('D010', N'Dr. Naglaa Adel', 'Dr. Naglaa Adel', 'Naglaa_Adel.com', 'pass555', '01055667788',1);
-- إدخال بيانات في جدول Rooms
INSERT INTO Rooms (Room_Num)
VALUES
    ('C42'),
    ('C45'),
    ('C22'),
    ('C37');

-- إدخال بيانات في جدول Subjects
INSERT INTO Subjects 
VALUES
    ('Sub001', N'Computer Programming', 1, 1, 1),
    ('Sub002', N'Data Structures', 2, 1, 2),
    ('Sub003', N'Advanced Mathematics', 3, 1, 3),
    ('Sub004', N'Artificial Intelligence', 1, 1, 1),
    ('Sub005', N'Computer Networks', 4, 1, 2),
    ('Sub006', N'System Analysis', 2, 1, 3),
    ('Sub007', N'Database Systems', 5, 1, 1),
    ('Sub008', N'Operating Systems', 6, 1, 2),
    ('Sub009', N'Software Engineering', 7, 1, 2),
    ('Sub00A', N'Information Security', 8, 1, 3);
-- إدخال بيانات في جدول Lecture_S

INSERT INTO Lecture 
VALUES
('Computer Programming',1, 1, '08:00:00', '10:00:00'),  -- Computer Programming
('Data Structures', 2,1, '10:30:00', '12:00:00'),  -- Data Structures
('Advanced Mathematics',3, 1, '12:00:00', '14:00:00'),  -- Advanced Mathematics
('Artificial Intelligence',4, 2, '08:00:00', '10:00:00'),  -- Artificial Intelligence
('Computer Networks',5, 2, '10:30:00', '12:00:00'),  -- Computer Networks
('System Analysis', 6,2, '12:00:00', '14:00:00'),  -- System Analysis
('Database Systems',7, 3, '08:00:00', '10:00:00'),  -- Database Systems
('Operating Systems',8, 3, '10:30:00', '12:00:00'),  -- Operating Systems
('Software Engineering',9, 4, '12:00:00', '14:00:00'),  -- Software Engineering
('Information Security',10, 4, '08:00:00', '10:00:00'); -- Information Security

 