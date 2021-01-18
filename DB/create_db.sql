create database student_db;

use student_db;

create table discipline
(
    professor_name varchar(128) charset utf8 null,
    name           varchar(128) charset utf8 null,
    id             int auto_increment
        primary key
);

create index disciplines_id_index
    on discipline (id);


create table semester
(
    name varchar(128) charset utf8 not null,
    id   int auto_increment not null
        primary key
);

create table discipline_semester
(
    id            int auto_increment not null
		primary key,
    semester_id   int not null,
    discipline_id int not null,
    constraint discipline_semester_disciplines_id_fk
        foreign key (discipline_id) references discipline (id),
    constraint discipline_semester_semester_id_fk
        foreign key (semester_id) references semester (id)
);

create table student
(
    id          int auto_increment
        primary key,
    name        varchar(128) charset utf8 not null,
    semester_id int not null,
    constraint student_semester_id_fk
        foreign key (semester_id) references semester (id)
);

create table student_scores
(
    id                     int auto_increment
        primary key,
    discipline_semester_id int           not null,
    student_id             int           not null,
    score                  decimal(2, 1) null,
    constraint student_score_discipline_semester_id_fk
        foreign key (discipline_semester_id) references discipline_semester (id),
    constraint student_score_student_id_fk
        foreign key (student_id) references student (id)
);