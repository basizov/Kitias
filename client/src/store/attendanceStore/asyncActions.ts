import {attendanceActions, AttendanceActionType} from "./index";
import {ThunkAction} from "redux-thunk/es/types";
import {API} from "../../api";
import {ServerErrorType} from "../../model/ServerError";

type AsyncThunkType = ThunkAction<Promise<void>,
  null,
  unknown,
  AttendanceActionType>;

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
