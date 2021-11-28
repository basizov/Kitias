import {attendanceActions, AttendanceActionType} from "./index";
import {ThunkAction} from "redux-thunk/es/types";
import {API} from "../../api";
import {ServerErrorType} from "../../model/ServerError";
import {subjectActions, SubjectActionType} from "../subjectStore";

type AsyncThunkType = ThunkAction<Promise<void>,
  null,
  unknown,
  AttendanceActionType | SubjectActionType>;

export const getShedulers = () : AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoading(true));
    try {
      const response = await API.attendance.shedulers();

      if (response) {
        dispatch(attendanceActions.setShedulers(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(attendanceActions.setLoading(false));
    }
  }
};

export const getAttendances = (id: string) : AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoading(true));
    try {
      const response = await API.attendance.attendances(id);

      if (response) {
        dispatch(attendanceActions.setAttendances(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(attendanceActions.setLoading(false));
    }
  }
};

export const getAttendanceSubjects = (id: string) : AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoading(true));
    try {
      const response = await API.attendance.subjects(id);

      console.log(response)
      if (response) {
        dispatch(subjectActions.setSubjects(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(attendanceActions.setLoading(false));
    }
  }
};
