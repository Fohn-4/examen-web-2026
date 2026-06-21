CREATE DATABASE event_management;
USE event_management;

------------------
-- MASTER TABLE --
------------------

-- Store uploaded images used for activities, events, profiles pictures
CREATE TABLE image (
	UUID CHAR(32) PRIMARY KEY DEFAULT (REPLACE(UUID(), '-', '')),
	image_url VARCHAR(255) UNIQUE NOT NULL,
	alt VARCHAR(255),
	upload_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Role entity for user's permissions
CREATE TABLE role (
	UUID CHAR(32) PRIMARY KEY DEFAULT (REPLACE(UUID(), '-', '')),
	name VARCHAR(255) NOT NULL UNIQUE
);


-- User entity
CREATE TABLE app_user (
	UUID CHAR(32) PRIMARY KEY DEFAULT (REPLACE(UUID(), '-', '')),
	first_name VARCHAR(100) NOT NULL,
	last_name VARCHAR(100) NOT NULL,
	phone_number VARCHAR(15),
	location VARCHAR(150),
	email VARCHAR(255) NOT NULL UNIQUE,
	password_hash VARCHAR(255) NOT NULL,
	image CHAR(32) NULL,
	FOREIGN KEY (image) REFERENCES image(UUID) ON DELETE SET NULL
);

-- Entity used to invite a person to register
CREATE TABLE invitation (
	UUID CHAR(32) PRIMARY KEY DEFAULT (REPLACE(UUID(), '-', '')),
	email VARCHAR(255) NOT NULL,
	token VARCHAR(64) NOT NULL UNIQUE,
	expire_at DATETIME NOT NULL,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	created_by CHAR(32) NULL, -- UUID
	is_used BOOLEAN DEFAULT FALSE,
	FOREIGN KEY (created_by) REFERENCES app_user(UUID) ON DELETE SET NULL
);

-- Event entity
CREATE TABLE event (
	UUID CHAR(32) PRIMARY KEY DEFAULT (REPLACE(UUID(), '-', '')),
	name VARCHAR(100) NOT NULL,
	location VARCHAR(150),
	start_date DATE NOT NULL,
	end_date DATE NOT NULL,
	organizer_url VARCHAR(255),
	is_public BOOLEAN DEFAULT TRUE,
	description TEXT,
	image CHAR(32) NULL, -- UUID
	thumbnail CHAR(32) NULL, -- UUID
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	created_by CHAR(32) NULL, -- UUID
	updated_by CHAR(32) NULL, -- UUID
	FOREIGN KEY (created_by) REFERENCES app_user(UUID) ON DELETE SET NULL,
	FOREIGN KEY (updated_by) REFERENCES app_user(UUID) ON DELETE SET NULL,
	FOREIGN KEY (image) REFERENCES image(UUID) ON DELETE SET NULL,
	FOREIGN KEY (thumbnail) REFERENCES image(UUID) ON DELETE SET NULL,
	CHECK (end_date >= start_date),
	INDEX idx_event_name (name),
	INDEX idx_event_location (location),
	INDEX idx_event_start_date (start_date)
);

	-- Activity entity, 
CREATE TABLE activity (
	UUID CHAR(32) PRIMARY KEY DEFAULT (REPLACE(UUID(), '-', '')),
	name VARCHAR(100) NOT NULL,
	image CHAR(32) NULL, -- UUID
	thumbnail CHAR(32) NULL, -- UUID
	description TEXT,
	is_active BOOLEAN DEFAULT TRUE,
	FOREIGN KEY (image) REFERENCES image(UUID) ON DELETE SET NULL,
	FOREIGN KEY (thumbnail) REFERENCES image(UUID) ON DELETE SET NULL,
	INDEX idx_activity_name(name)
);

----------------
-- LINK TABLE --
----------------

--  Assign roles to users (many to many)
CREATE TABLE app_user_role (
	app_user_UUID CHAR(32) NOT NULL,
	role_UUID CHAR(32) NOT NULL,
	PRIMARY KEY (app_user_UUID, role_UUID),
	FOREIGN KEY(app_user_UUID) REFERENCES app_user(UUID) ON DELETE CASCADE,
	FOREIGN KEY (role_UUID) REFERENCES role(UUID) ON DELETE CASCADE
);

-- Asign activity to event (many to many)
CREATE TABLE event_activity (
	event_UUID CHAR(32) NOT NULL,
	activity_UUID CHAR(32) NOT NULL,
	PRIMARY KEY (event_UUID, activity_UUID),
	FOREIGN KEY (event_UUID) REFERENCES event(UUID) ON DELETE CASCADE,
	FOREIGN KEY (activity_UUID) REFERENCES activity(UUID) ON DELETE CASCADE
);

-- assign attendance to events (many to many)
CREATE TABLE event_attendance (
	event_UUID CHAR(32) NOT NULL,
	app_user_UUID CHAR(32) NOT NULL,
	arrival_at DATETIME,
	departure_at DATETIME,
	PRIMARY KEY (event_UUID, app_user_UUID),
	FOREIGN KEY (event_UUID) REFERENCES event(UUID) ON DELETE CASCADE,
	FOREIGN KEY (app_user_UUID) REFERENCES app_user(UUID) ON DELETE CASCADE
);