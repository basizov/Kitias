export type AttendenceType = {
  id: string;
  attended: string;
  type: string;
  date: string;
  theme: string;
  score: string;
  fullName: string;
};

export type AttendancesByStudents = {
  [key: string]: AttendenceType[];
};
