import {instance} from "./instance";
import {SignInType} from "../model/User/SignInModel";
import {AxiosRequestConfig, AxiosResponse} from "axios";
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
import {CreateStudentAttendanceType} from "../model/Attendance/CreateStudentAttendance";
import {StudentAttendanceResultType} from "../model/Attendance/StudentAttendaceModel";
import {saveAs} from "file-saver";
import {SignUpType} from "../model/User/SugnUpModel";
import {UpdateSAttendanceType} from "../model/Attendance/UpdateStudentAttendance";
import {GroupType, StudentInGroupType} from "../model/Group/GroupModel";
import {CreateGroupType} from "../model/Group/CreateGroup";

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

enum Paths {
  IS_AUTH_PATH = '/Auth',
  LOGOUT_PATH = '/Auth/logout',
  SIGN_IN_PATH = '/Auth/signIn',
  SIGN_UP_PATH = '/Auth/signUp',
  TAKE_GROUP = '/Group',
  TAKE_SUBJECTS = '/Teacher/subjects',
  TAKE_SUBJECTS_INFOS = '/Teacher/subjectsInfos',
  TAKE_SHEDULERS_PATH = '/AttendanceScheduler',
  TAKE_ATTENDANCES_PATH = '/AttendanceScheduler/',
  TAKE_GRADES_PATH = '/AttendanceScheduler/attendances/grades',
  CREATE_ATTENDANCES_PATH = '/AttendanceScheduler',
  TAKE_SUBJECT = '/Subject'
};

const requests = {
  get: <T>(url: string) => instance.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) => instance.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => instance.put<T>(url, body).then(responseBody),
  delete: (url: string, body?: {}) => instance.delete(url, body).then(responseBody),
  getBlob: (url: string) => instance.request<any>({
    url: url,
    method: "GET",
    responseType: 'blob'
  } as AxiosRequestConfig)
    .then((response: AxiosResponse) => {
      if (response && response.headers) {
        const header = response.headers['content-disposition'];

        if (header) {
          const splitHeader = header.split(';');

          if (splitHeader) {
            const filenameHeader = splitHeader
              .find((n: any) => n.includes('filename='));

            if (filenameHeader) {
              const filename = filenameHeader
                .replace('filename=', '')
                .trim();
              const url = window.URL
                .createObjectURL(new Blob([response.data]));

              saveAs(url, filename);
            }
          }
        }
      }
    })
};

const auth = {
  isAuth: () => requests.get<string[]>(Paths.IS_AUTH_PATH),
  logout: () => requests.get<string>(Paths.LOGOUT_PATH),
  signIn: (payload: SignInType) => requests.post<string[]>(Paths.SIGN_IN_PATH, payload),
  signUp: (payload: SignUpType) => requests.post(Paths.SIGN_UP_PATH, payload)
};

const attendance = {
  shedulers: () => requests.get<ShedulerListType[]>(Paths.TAKE_SHEDULERS_PATH),
  grades: () => requests.get<string[]>(Paths.TAKE_GRADES_PATH),
  studentsGroup: (id: string) => requests.get<ShedulerStudentsGroupType>(`${Paths.TAKE_SHEDULERS_PATH}/${id}/students`),
  details: (id: string) => requests.get<ShedulerListType>(`${Paths.TAKE_SHEDULERS_PATH}/${id}`),
  attendances: (id: string) => requests.get<AttendancesByStudents[]>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/attendances`),
  update: (id: string, payload: UpdateAttendaceType) => requests.put<AttendenceType>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/attendances`, payload),
  subjects: (id: string) => requests.get<SubjectType[]>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/subjects`),
  export: (id: string) => requests.getBlob(`${Paths.TAKE_ATTENDANCES_PATH}${id}/export`),
  createSheduler: (payload: CreateShedulerTYpe) => requests
    .post<ShedulerType>(Paths.CREATE_ATTENDANCES_PATH, payload),
  updateSAttendance: (id: string, payload: UpdateSAttendanceType) => requests
    .put(`${Paths.TAKE_SHEDULERS_PATH}/${id}/studentAttendances`, payload),
  updateSheduler: (id: string, payload: CreateShedulerTYpe) => requests
    .put<ShedulerType>(`${Paths.CREATE_ATTENDANCES_PATH}/${id}`, payload),
  deleteSheduler: (id: string) => requests.delete(`${Paths.CREATE_ATTENDANCES_PATH}/${id}`),
  createSAttendance: (id: string, payload: CreateStudentAttendanceType[]) => requests
    .post(`${Paths.TAKE_ATTENDANCES_PATH}${id}/studentAttendances`, payload),
  updateStudentAttendance: (id: string, payload: CreateStudentAttendanceType[]) => requests
    .put(`${Paths.TAKE_ATTENDANCES_PATH}${id}/studentAttendances/all`, payload),
  studentAttendances: (id: string) => requests
    .get<StudentAttendanceResultType[]>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/studentAttendances`),
  createAttendances: (id: string, payload: CreateAttendanceType[]) => requests
    .post<AttendenceType[]>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/attendances`, payload),
  updateAttendances: (id: string, payload: CreateAttendanceType[]) => requests
    .put<AttendenceType[]>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/attendances/all`, payload)
};

const subject = {
  subjects: (payload: string) => requests.get<SubjectType[]>(`${Paths.TAKE_SUBJECTS}/${payload}`),
  sheduler: (name: string) => requests.get<ShedulerType>(`${Paths.TAKE_SUBJECT}/${name}/sheduler`),
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
  groups: () => requests.get<GroupType[]>(`${Paths.TAKE_GROUP}/withStudents`),
  create: (payload: CreateGroupType) => requests.post<GroupType>(Paths.TAKE_GROUP, payload),
  deleteGroup: (id: string) => requests.delete(`${Paths.TAKE_GROUP}/${id}`),
  students: (id: string, payload: string[]) => requests
    .post<GroupType[]>(`${Paths.TAKE_GROUP}/${id}/students`, payload),
  groupNames: () => requests.get<GroupName[]>(`${Paths.TAKE_GROUP}/names`),
  groupStudentsNames: (id: string) => requests
    .get<StudentInGroupType[]>(`${Paths.TAKE_GROUP}/${id}/students/names`)
};

export const API = {
  auth,
  attendance,
  subject,
  group
};
