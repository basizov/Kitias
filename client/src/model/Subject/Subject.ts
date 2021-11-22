export type SubjectType = {
  id: string;
  name: string;
  type: string;
  time: string;
  date: string;
  week: string;
  theme: string;
  day: string;
};

type SubSubjectInfoType = {
  [key: string]: string[];
};

export type SubjectInfoType = {
  [key: string]: SubSubjectInfoType;
};
