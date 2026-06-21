START TRANSACTION;

-- ------------------
-- ROLES
-- ------------------
INSERT INTO role (name) VALUES
('SuperAdmin'),
('Admin'),
('Member'),
('Cook'),
('CampManager');

-- ------------------
-- USERS
-- ------------------
INSERT INTO app_user (
	first_name, last_name, phone_number, location,
	email, password_hash
) VALUES
('Gandalf', 'The Grey', '0000000001', 'Middle-Earth', 'gandalf@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC'),
('Merlin', 'Ambrosius', '0000000002', 'Camelot', 'merlin@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC'),
('Arthur', 'Pendragon', '0000000003', 'Camelot', 'arthur@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC'),
('Tony', 'Stark', '0000000004', 'New York', 'tony.stark@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC'),
('Bruce', 'Wayne', '0000000005', 'Gotham', 'bruce.wayne@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC'),
('Lara', 'Croft', '0000000006', 'London', 'lara.croft@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC'),
('Geralt', 'Of Rivia', '0000000007', 'Kaer Morhen', 'geralt@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC'),
('Jon', 'Snow', '0000000008', 'Winterfell', 'jon.snow@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC'),
('Leia', 'Organa', '0000000009', 'Alderaan', 'leia.organa@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC'),
('Indiana', 'Jones', '0000000010', 'Princeton', 'indiana.jones@example.com', '$2a$11$75dNBlFTdFYObKOwp6unoe0a63MsnasaKXMy8DEC4N0YPKlENH1sC');

-- ------------------
-- USER ↔ ROLE (many to many)
-- ------------------

-- SuperAdmin
INSERT INTO app_user_role (app_user_UUID, role_UUID)
SELECT u.UUID, r.UUID
FROM app_user u, role r
WHERE u.email = 'gandalf@example.com'
  AND r.name = 'SuperAdmin';

-- Admins
INSERT INTO app_user_role (app_user_UUID, role_UUID)
SELECT u.UUID, r.UUID
FROM app_user u, role r
WHERE u.email IN ('merlin@example.com', 'arthur@example.com')
  AND r.name = 'Admin';

-- Base role for everyone
INSERT INTO app_user_role (app_user_UUID, role_UUID)
SELECT u.UUID, r.UUID
FROM app_user u, role r
WHERE r.name = 'Member';

-- Cooks
INSERT INTO app_user_role (app_user_UUID, role_UUID)
SELECT u.UUID, r.UUID
FROM app_user u, role r
WHERE u.email IN ('lara.croft@example.com', 'bruce.wayne@example.com')
  AND r.name = 'Cook';

-- Camp managers
INSERT INTO app_user_role (app_user_UUID, role_UUID)
SELECT u.UUID, r.UUID
FROM app_user u, role r
WHERE u.email IN ('tony.stark@example.com', 'indiana.jones@example.com')
  AND r.name = 'CampManager';

-- ------------------
-- ACTIVITIES
-- ------------------
INSERT INTO activity (name, description, is_active) VALUES
('Sword Fighting', 'Medieval sword techniques', TRUE),
('Archery', 'Bow and arrow practice', TRUE),
('Blacksmithing', 'Forging weapons and armor', TRUE),
('Medieval Cooking', 'Traditional medieval recipes', TRUE),
('Falconry', 'Birds of prey demonstration', TRUE),
('Storytelling', 'Legends and myths', TRUE),
('Tournament', 'Knight competitions', TRUE),
('Alchemy', 'Alchemy experiments', FALSE);

-- ------------------
-- EVENTS
-- ------------------
INSERT INTO event (
	name, location, start_date, end_date,
	is_public, description, created_by, updated_by
) VALUES
(
	'Battle of Helm’s Deep',
	'Rohan',
	'2025-05-12',
	'2025-05-15',
	TRUE,
	'Epic battle reenactment',
	(SELECT UUID FROM app_user WHERE email = 'gandalf@example.com'),
	(SELECT UUID FROM app_user WHERE email = 'gandalf@example.com')
),
(
	'Knights Tournament',
	'Camelot',
	'2025-06-01',
	'2025-06-02',
	TRUE,
	'Grand medieval tournament',
	(SELECT UUID FROM app_user WHERE email = 'arthur@example.com'),
	(SELECT UUID FROM app_user WHERE email = 'arthur@example.com')
),
(
	'Alchemy Workshop',
	'Merlin’s Tower',
	'2025-04-15',
	'2025-04-16',
	FALSE,
	'Secrets of alchemy',
	(SELECT UUID FROM app_user WHERE email = 'merlin@example.com'),
	(SELECT UUID FROM app_user WHERE email = 'merlin@example.com')
);

-- ------------------
-- EVENT ↔ ACTIVITY (many to many)
-- ------------------
INSERT INTO event_activity (event_UUID, activity_UUID)
SELECT e.UUID, a.UUID
FROM event e, activity a
WHERE e.name = 'Battle of Helm’s Deep'
  AND a.name IN ('Sword Fighting', 'Archery', 'Tournament');

INSERT INTO event_activity (event_UUID, activity_UUID)
SELECT e.UUID, a.UUID
FROM event e, activity a
WHERE e.name = 'Knights Tournament'
  AND a.name IN ('Sword Fighting', 'Tournament');

INSERT INTO event_activity (event_UUID, activity_UUID)
SELECT e.UUID, a.UUID
FROM event e, activity a
WHERE e.name = 'Alchemy Workshop'
  AND a.name = 'Alchemy';

-- ------------------
-- EVENT ATTENDANCE (many to many, horaires différents)
-- ------------------
INSERT INTO event_attendance (event_UUID, app_user_UUID, arrival_at)
SELECT e.UUID, u.UUID, '2025-05-12 09:00:00'
FROM event e, app_user u
WHERE e.name = 'Battle of Helm’s Deep'
  AND u.email IN ('jon.snow@example.com', 'geralt@example.com');

INSERT INTO event_attendance (event_UUID, app_user_UUID, arrival_at)
SELECT e.UUID, u.UUID, '2025-05-12 14:00:00'
FROM event e, app_user u
WHERE e.name = 'Battle of Helm’s Deep'
  AND u.email IN ('lara.croft@example.com', 'bruce.wayne@example.com');

INSERT INTO event_attendance (event_UUID, app_user_UUID, arrival_at)
SELECT e.UUID, u.UUID, '2025-05-13 10:30:00'
FROM event e, app_user u
WHERE e.name = 'Battle of Helm’s Deep'
  AND u.email = 'indiana.jones@example.com';

COMMIT;
