export type GroupType = {
  id: string;
  course: number;
  number: string;
  students: StudentInGroupType[]
};

export type StudentInGroupType = {
  id: string;
  fullName: string;
};