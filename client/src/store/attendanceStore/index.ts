import {InferActionType} from "../index";
import {
  ShedulerListType,
  TeacherShedulerType
} from "../../model/Attendance/ShedulerList";
import {AttendancesByStudents} from "../../model/Attendance/Attendence";
import {GroupName} from "../../model/Group/GroupNames";
import {StudentAttendanceResultType} from "../../model/Attendance/StudentAttendaceModel";
import {StudentInGroupType} from "../../model/Group/GroupModel";

const initialState = {
  shedulers: [] as ShedulerListType[],
  teacherShedulers: [] as TeacherShedulerType[],
  grades: [] as string[],
  sAttendances: [] as StudentAttendanceResultType[],
  shedulerGroup: '',
  selectedSheduler: '',
  groupsNames: [] as GroupName[],
  groupStudents: [] as StudentInGroupType[],
  attendances: [] as AttendancesByStudents[],
  error: '',
  loadingInitial: false,
  loadingHelper: false,
  loadingSA: false,
  loading: false
};

export const attendanceReducer = (state = initialState, action: AttendanceActionType) => {
  switch (action.type) {
    case 'SET_SHEDULERS':
      return {...state, shedulers: action.payload};
    case 'SET_TEACHERS_SHEDULERS':
      return {...state, teacherShedulers: action.payload};
    case 'SET_GRADES':
      return {...state, grades: action.payload};
    case 'SET_SATTENDACES':
      return {...state, sAttendances: action.payload};
    case 'SET_SELECTED_SHEDULER':
      return {...state, selectedSheduler: action.payload};
    case 'SET_GROUP_SHEDULER':
      return {...state, shedulerGroup: action.payload};
    case "SET_ATTENDANCES":
      return {...state, attendances: action.payload};
    case "SET_GROUPS_NAMES":
      return {...state, groupsNames: action.payload};
    case "SET_GROUP_STUDENTS":
      return {...state, groupStudents: action.payload};
    case 'SET_SHEDULER_LOADING':
      return {...state, loading: action.payload};
    case 'SET_SHEDULER_LOADING_HELPER':
      return {...state, loadingHelper: action.payload};
    case 'SET_SHEDULER_LOADING_SA':
      return {...state, loadingSA: action.payload};
    case 'SET_SHEDULER_LOADING_INITIAL':
      return {...state, loadingInitial: action.payload};
    case 'SET_ATTENDANCE_ERROR':
      return {...state, error: action.payload};
    default:
      return {...state};
  }
};

export type AttendanceActionType = InferActionType<typeof attendanceActions>;

export const attendanceActions = {
  setShedulers: (payload: ShedulerListType[]) => ({
    type: 'SET_SHEDULERS',
    payload
  } as const),
  setTeacherShedulers: (payload: TeacherShedulerType[]) => ({
    type: 'SET_TEACHERS_SHEDULERS',
    payload
  } as const),
  setGrades: (payload: string[]) => ({
    type: 'SET_GRADES',
    payload
  } as const),
  setSAttendances: (payload: StudentAttendanceResultType[]) => ({
    type: 'SET_SATTENDACES',
    payload
  } as const),
  setShedulerStudentsGroup: (payload: string) => ({
    type: 'SET_GROUP_SHEDULER',
    payload
  } as const),
  setSelectedSheduler: (payload: string) => ({
    type: 'SET_SELECTED_SHEDULER',
    payload
  } as const),
  setGroupsNames: (payload: GroupName[]) => ({
    type: 'SET_GROUPS_NAMES',
    payload
  } as const),
  setGroupStudents: (payload: StudentInGroupType[]) => ({
    type: 'SET_GROUP_STUDENTS',
    payload
  } as const),
  setAttendances: (payload: AttendancesByStudents[]) => ({
    type: 'SET_ATTENDANCES',
    payload
  } as const),
  setError: (payload: string) => ({
    type: 'SET_ATTENDANCE_ERROR',
    payload
  } as const),
  setLoading: (payload: boolean) => ({
    type: 'SET_SHEDULER_LOADING',
    payload
  } as const),
  setLoadingHelper: (payload: boolean) => ({
    type: 'SET_SHEDULER_LOADING_HELPER',
    payload
  } as const),
  setLoadingSA: (payload: boolean) => ({
    type: 'SET_SHEDULER_LOADING_SA',
    payload
  } as const),
  setLoadingInitial: (payload: boolean) => ({
    type: 'SET_SHEDULER_LOADING_INITIAL',
    payload
  } as const)
};
