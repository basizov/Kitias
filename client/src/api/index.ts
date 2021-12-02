import {instance} from "./instance";
import {SignInType} from "../model/User/SignInModel";
import {AxiosResponse} from "axios";
import {ShedulerListType} from "../model/Attendance/ShedulerList";
import {
  AttendancesByStudents, AttendenceType,
} from "../model/Attendance/Attendence";
import {SubjectInfoType, SubjectType} from "../model/Subject/Subject";
import {UpdateAttendaceType} from "../model/Attendance/UpdateAttendace";
import {CreateSubjectType} from "../model/Subject/CreateSubjectModel";
import {
  DeleteSubjectType,
  UpdateSubjectType
} from "../model/Subject/UpdateSubjectModel";
import {GroupName} from "../model/Group/GroupNames";
import {
  CreateShedulerTYpe,
  ShedulerType
} from "../model/Attendance/CreateShedulerModel";
import {CreateAttendanceType} from "../model/Attendance/CreateAttendanceModel";
import {ShedulerStudentsGroupType} from "../model/Attendance/ShedulerStudentsGroup";

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

enum Paths {
  IS_AUTH_PATH = '/Auth',
  SIGN_IN_PATH = '/Auth/signIn',
  TAKE_GROUP = '/Group',
  TAKE_SUBJECTS = '/Teacher/subjects',
  TAKE_SUBJECTS_INFOS = '/Teacher/subjectsInfos',
  TAKE_SHEDULERS_PATH = '/AttendanceScheduler',
  TAKE_ATTENDANCES_PATH = '/AttendanceScheduler/',
  CREATE_ATTENDANCES_PATH = '/AttendanceScheduler',
  TAKE_SUBJECT = '/Subject'
};

const requests = {
  get: <T>(url: string) => instance.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) => instance.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => instance.put<T>(url, body).then(responseBody),
  delete: (url: string, body?: {}) => instance.delete(url, body).then(responseBody)
};

const auth = {
  isAuth: () => requests.get<string>(Paths.IS_AUTH_PATH),
  signIn: (payload: SignInType) => requests.post(Paths.SIGN_IN_PATH, payload)
};

const attendance = {
  shedulers: () => requests.get<ShedulerListType[]>(Paths.TAKE_SHEDULERS_PATH),
  studentsGroup: (id: string) => requests.get<ShedulerStudentsGroupType>(`${Paths.TAKE_SHEDULERS_PATH}/${id}/students`),
  details: (id: string) => requests.get<ShedulerListType>(`${Paths.TAKE_SHEDULERS_PATH}/${id}`),
  attendances: (id: string) => requests.get<AttendancesByStudents[]>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/attendances`),
  update: (id: string, payload: UpdateAttendaceType) => requests.put<AttendenceType>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/attendances`, payload),
  subjects: (id: string) => requests.get<SubjectType[]>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/subjects`),
  createSheduler: (payload: CreateShedulerTYpe) => requests
    .post<ShedulerType>(Paths.CREATE_ATTENDANCES_PATH, payload),
  updateSheduler: (id: string, payload: CreateShedulerTYpe) => requests
    .put<ShedulerType>(`${Paths.CREATE_ATTENDANCES_PATH}/${id}`, payload),
  deleteSheduler: (id: string) => requests.delete(`${Paths.CREATE_ATTENDANCES_PATH}/${id}`),
  createAttendances: (id: string, payload: CreateAttendanceType[]) => requests
    .post<AttendenceType[]>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/attendances`, payload)
};

const subject = {
  subjects: (payload: string) => requests.get<SubjectType[]>(`${Paths.TAKE_SUBJECTS}/${payload}`),
  subjectsNames: () => requests.get<string[]>(`${Paths.TAKE_SUBJECTS}/names`),
  subjectsInfos: () => requests.get<SubjectInfoType[]>(Paths.TAKE_SUBJECTS_INFOS),
  allSubjects: () => requests.get<SubjectType[]>(Paths.TAKE_SUBJECTS),
  create: (payload: CreateSubjectType[]) => requests.post<SubjectType[]>(Paths.TAKE_SUBJECT, payload),
  update: (id: string, payload: CreateSubjectType) => requests.put<SubjectType>(
    `${Paths.TAKE_SUBJECT}/${id}`, payload
  ),
  delete: (id: string) => requests.delete(`${Paths.TAKE_SUBJECT}/${id}`),
  updateName: (payload: UpdateSubjectType) =>
    requests.put<SubjectType[]>(Paths.TAKE_SUBJECT, payload),
  deleteName: (payload: DeleteSubjectType) =>
    requests.delete(`${Paths.TAKE_SUBJECT}/name/${payload.name}`)
};

const group = {
  groupNames: () => requests.get<GroupName[]>(`${Paths.TAKE_GROUP}/names`),
  groupStudentsNames: (id: string) => requests
    .get<string[]>(`${Paths.TAKE_GROUP}/${id}/students/names`)
};

export const API = {
  auth,
  attendance,
  subject,
  group
};
