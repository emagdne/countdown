DROP TABLE IF EXISTS todo_items;
DROP TABLE IF EXISTS users;

CREATE TABLE users (
	id INTEGER PRIMARY KEY AUTOINCREMENT, 
	first_name VARCHAR2(50) NOT NULL,
	last_name VARCHAR2(50),
	email VARCHAR2(320) NOT NULL, 
	hash CHAR(68) NOT NULL
);

CREATE TABLE todo_items (
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	title VARCHAR2(50) NOT NULL,
	description VARCHAR(200),
	start_date DATETIME NOT NULL,
	due_date DATETIME NOT NULL,
	owner INTEGER NOT NULL,
	assigned_to INTEGER NOT NULL,
	completed BOOLEAN DEFAULT 0,
	FOREIGN KEY (owner) REFERENCES users(id),
	FOREIGN KEY (assigned_to) REFERENCES users(id)
);
