import {instance} from "./instance";
import {SignInType} from "../model/User/SignInModel";
import {AxiosResponse} from "axios";
import {ShedulerListType} from "../model/Attendance/ShedulerList";
import {AttendenceType} from "../model/Attendance/Attendence";
import {SubjectInfoType, SubjectType} from "../model/Subject/Subject";

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

enum Paths {
  IS_AUTH_PATH = '/Auth',
  SIGN_IN_PATH = '/Auth/signIn',
  TAKE_SUBJECTS = '/Teacher/subjects',
  TAKE_SUBJECTS_INFOS = '/Teacher/subjectsInfos',
  TAKE_SHEDULERS_PATH = '/Teacher/subjectsInfos',
  TAKE_ATTENDANCES_PATH = '/AttendanceScheduler/'
};

const requests = {
  get: <T>(url: string) => instance.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) => instance.post<T>(url, body).then(responseBody)
};

const auth = {
  isAuth: () => requests.get<string>(Paths.IS_AUTH_PATH),
  signIn: (payload: SignInType) => requests.post(Paths.SIGN_IN_PATH, payload)
};

const attendance = {
  shedulers: () => requests.get<ShedulerListType[]>(Paths.TAKE_SHEDULERS_PATH),
  attendances: (id: string) => requests.get<AttendenceType[]>(`${Paths.TAKE_ATTENDANCES_PATH}${id}/attendances`)
};

const subject = {
  subjects: (payload: string) => requests.get<SubjectType[]>(`${Paths.TAKE_SUBJECTS}/${payload}`),
  subjectsInfos: () => requests.get<SubjectInfoType[]>(Paths.TAKE_SUBJECTS_INFOS)
};

export const API = {
  auth,
  attendance,
  subject
};
