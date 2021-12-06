export type StudentAttendanceResultType = {
  id: string;
  studentName: string,
  raiting: string,
  grade: string,
  lectures: TypeInfoType[];
  laborotories: TypeInfoType[];
  practises: TypeInfoType[];
};

export type TypeInfoType = {
  id: string;
  attended: string;
  score: string;
};