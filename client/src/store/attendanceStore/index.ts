import {InferActionType} from "../index";
import {ShedulerListType} from "../../model/Attendance/ShedulerList";
import {AttendancesByStudents} from "../../model/Attendance/Attendence";

const initialState = {
  shedulers: [] as ShedulerListType[],
  attendances: [] as AttendancesByStudents[],
  error: '',
  loading: false
};

export const attendanceReducer = (state = initialState, action: AttendanceActionType) => {
  switch (action.type) {
    case 'SET_SHEDULERS':
      return {...state, shedulers: action.payload};
    case "SET_ATTENDANCES":
      return {...state, attendances: action.payload};
    case 'SET_SHEDULER_LOADING':
      return {...state, loading: action.payload};
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
  } as const)
};
