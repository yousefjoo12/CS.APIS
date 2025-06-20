-- إدخال بيانات في جدول Faculty
INSERT INTO Faculty 
VALUES 
    ('CS', 'Computer Science'),
    ('ENG', 'Engineering');

-- إدخال بيانات في جدول FacultyYear
INSERT INTO FacultyYear  
VALUES
    ('2025',1),
    ('2026',1),
    ('2027',1),
    ('2028',1);

-- إدخال بيانات في جدول FacultyYearSemister
INSERT INTO FacultyYearSemister
VALUES 
    ('S1', 'Semester 1', 1),
    ('S2', 'Semester 2', 1),
	('S1', 'Semester 1', 2),
    ('S2', 'Semester 2', 2),
	('S1', 'Semester 1', 3),
    ('S2', 'Semester 2', 3),
	('S1', 'Semester 1', 4),
    ('S2', 'Semester 2', 4);

-- إدخال بيانات في جدول Students
 INSERT INTO Students  
VALUES 
    ('S001', N'أحمد علي', 'Ahmed Ali', 'Ahmed_Ali.com', 'image1.jpg', '0123456789', NULL, 1, 1, 1),
    ('S002', N'سارة محمد', 'Sara Mohamed', 'Sara_Mohamed.com', 'image2.jpg', '0987654321', NULL,  1, 1, 1),
    ('S003', N'محمد سمير', 'Mohamed Samir', 'Mohamed_Samir.com', 'image3.jpg', '0111222333', NULL,  1, 1, 1),
    ('S004', N'ليلى حسن', 'Laila Hassan', 'Laila_Hassan.com', 'image4.jpg', '0223344556', NULL,  1, 1, 1),
    ('S005', N'كريم يوسف', 'Karim Youssef', 'Karim_Youssef.com', 'image5.jpg', '0334455667', NULL, 1, 1, 1),
    ('S006', N'هدى عبد الله', 'Huda Abdullah', 'Huda_Abdullah.com', 'image6.jpg', '0445566778', NULL, 1, 1, 1),
    ('S007', N'مازن إبراهيم', 'Mazen Ibrahim', 'Mazen_Ibrahim.com', 'image7.jpg', '0556677889', NULL, 1, 1, 1),
    ('S008', N'ياسمين صلاح', 'Yasmin Salah', 'Yasmin_Salah.com', 'image8.jpg', '0667788990', NULL,  1, 1, 1),
    ('S009', N'نور الدين محمود', 'Nour El Din Mahmoud', 'Nour_Mahmoud.com', 'image9.jpg', '0778899001', NULL, 1, 1, 1),
    ('S010', N'رنا خالد', 'Rana Khaled', 'Rana_Khaled.com', 'image10.jpg', '0889900112', NULL, 1, 1, 1);


-- إدخال بيانات في جدول Doctors
INSERT INTO Doctors  
VALUES
    ('D001', N'د. محمد يوسف', 'Dr. Mohamed Youssef', ' Mohamed_Youssef.com', 'pass123', '01123456789',1),
    ('D002', N'د. ندى مصطفى', 'Dr. Nada Mostafa', 'Nada_Mostafa.com', 'pass456', '01098765432',1),
    ('D003', N'د. سامي إبراهيم', 'Dr. Sami Ibrahim', 'Sami_Ibrahim.com', 'pass789', '01234567890',1),
    ('D004', N'د. هالة فوزي', 'Dr. Hala Fawzy', 'Hala_Fawzy.com', 'pass321', '01987654321',1),
    ('D005', N'د. عمرو زكريا', 'Dr. Amr Zakaria', 'Amr_Zakaria.com', 'pass654', '01876543210',1),
    ('D006', N'د. ريم نبيل', 'Dr. Reem Nabil', 'Reem_Nabil.com', 'pass111', '01011223344',1),
    ('D007', N'د. كريم عبد الله', 'Dr. Karim Abdallah', 'Karim_Abdallah.com', 'pass222', '01022334455',2),
    ('D008', N'د. ياسمين حسين', 'Dr. Yasmin Hussein', 'Yasmin_Hussein.com', 'pass333', '01033445566',2),
    ('D009', N'د. حسام كمال', 'Dr. Hossam Kamal', 'Hossam_Kamal.com', 'pass444', '01044556677',2),
    ('D010', N'د. نجلاء عادل', 'Dr. Naglaa Adel', 'Naglaa_Adel.com', 'pass555', '01055667788',2);

-- إدخال بيانات في جدول Rooms
INSERT INTO Rooms (Room_Num)
VALUES
    ('C42'),
    ('C45'),
    ('C37');

-- إدخال بيانات في جدول Subjects
INSERT INTO Subjects 
VALUES
    ('Sub001',N'برمجة الحاسوب', 1,  1,  1),
    ('Sub002',N'الهندسة المدنية', 2,  1,  2),
    ('Sub003',N'الرياضيات المتقدمة', 3,  1,  3),
    ('Sub004',N'الذكاء الاصطناعي', 1, 1,  1),
    ('Sub005',N'الشبكات', 4, 1,  2),
    ('Sub006',N'تحليل النظم', 2,  2,  3),
    ('Sub007',N'قواعد البيانات', 5,  2,  1),
    ('Sub008',N'الفيزياء العامة', 6,  2,  2),
    ('Sub009',N'هندسة البرمجيات', 7,  2,  2),
    ('Sub00A',N'أمن المعلومات', 8,  2,  3);

	
INSERT INTO Studets_Subject 
VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5),
(1, 6),
(2, 1),
(2, 2),
(2, 3),
(2, 4),
(2, 5),
(2, 6),
(3, 1),
(3, 2),
(3, 3),
(3, 4),
(3, 5),
(3, 6),
(4, 1),
(4, 2),
(4, 3),
(4, 4),
(4, 5),
(4, 6),
(5, 1),
(5, 2),
(5, 3),
(5, 4),
(5, 5),
(5, 6),
(6, 1),
(6, 2),
(6, 3),
(6, 4),
(6, 5),
(6, 6);

-- إدخال بيانات في جدول Lecture_S
INSERT INTO Lecture 
VALUES
    ('L001', 1,'2025-05-10', 50),
    ('L002', 2,'2025-05-11', 50),
    ('L003', 3,'2025-05-12', 50),
    ('L004', 4,'2025-05-12', 50),
    ('L005', 5,'2025-05-10', 50),
    ('L006', 6,'2025-05-11', 50),
    ('L007', 7,'2025-05-12', 50),
    ('L008', 8,'2025-05-12', 50), 
    ('L009', 9,'2025-05-12', 50);

-- إدخال بيانات في جدول Attendance_T
INSERT INTO Attendance  
VALUES
(1, 1, '2025-05-10 00:00:00.0000000', 1),
(1, 1, '2025-05-17 00:00:00.0000000', 1),
(1, 1, '2025-05-24 00:00:00.0000000', 1),
(2, 1, '2025-05-11 00:00:00.0000000', 1),
(3, 1, '2025-05-12 00:00:00.0000000', 1),
(3, 1, '2025-05-19 00:00:00.0000000', 1),
(2, 1, '2025-05-18 00:00:00.0000000', 1),
(1, 1, '2025-05-03 00:00:00.0000000', 1),
(2, 1, '2025-05-25 00:00:00.0000000', 1),
(2, 1, '2025-06-01 00:00:00.0000000', 1),
(3, 1, '2025-05-26 00:00:00.0000000', 1),
(3, 1, '2025-06-02 00:00:00.0000000', 1);
