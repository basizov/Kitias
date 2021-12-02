import {attendanceActions, AttendanceActionType} from "./index";
import {ThunkAction} from "redux-thunk/es/types";
import {API} from "../../api";
import {ServerErrorType} from "../../model/ServerError";
import {subjectActions, SubjectActionType} from "../subjectStore";
import {UpdateAttendaceType} from "../../model/Attendance/UpdateAttendace";
import {CreateShedulerTYpe} from "../../model/Attendance/CreateShedulerModel";
import {CreateAttendanceType} from "../../model/Attendance/CreateAttendanceModel";

type AsyncThunkType = ThunkAction<Promise<void>,
  null,
  unknown,
  AttendanceActionType | SubjectActionType>;

export const getShedulers = (): AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoadingInitial(true));
    try {
      const response = await API.attendance.shedulers();

      if (response) {
        dispatch(attendanceActions.setShedulers(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(attendanceActions.setLoadingInitial(false));
    }
  }
};

export const getSheduler = (id: string): AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoadingInitial(true));
    try {
      const response = await API.attendance.details(id);

      if (response) {
        dispatch(attendanceActions.setSelectedSheduler(response.name));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(attendanceActions.setLoadingInitial(false));
    }
  }
};

export const createSheduler = (
  payload: CreateShedulerTYpe,
  attendances: CreateAttendanceType[]
): AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoading(true));
    try {
      const response = await API.attendance.createSheduler(payload);

      if (response) {
        const responseAttendances = await API.attendance.createAttendances(
          response.id,
          attendances
        );

        if (responseAttendances) {
          dispatch(attendanceActions.setError(''));
        }
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(attendanceActions.setLoading(false));
    }
  }
};

export const getGroupsNames = (): AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoading(true));
    try {
      const response = await API.group.groupNames();

      if (response) {
        dispatch(attendanceActions.setGroupsNames(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(attendanceActions.setLoading(false));
    }
  }
};

export const getGroupStudents = (id: string): AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoading(true));
    try {
      const response = await API.group.groupStudentsNames(id);

      if (response) {
        dispatch(attendanceActions.setGroupStudents(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(attendanceActions.setLoading(false));
    }
  }
};

export const getAttendances = (id: string): AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoadingInitial(true));
    try {
      const response = await API.attendance.attendances(id);

      if (response) {
        dispatch(attendanceActions.setAttendances(response));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      console.log(error)
    } finally {
      dispatch(attendanceActions.setLoadingInitial(false));
    }
  }
};

export const updateAttendance = (
  id: string,
  payload: UpdateAttendaceType
): AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoading(true));
    try {
      const response = await API.attendance.update(id, payload);

      if (response) {
        dispatch(attendanceActions.setError(''));
      }
    } catch (e) {
      const error = e as ServerErrorType;

      dispatch(attendanceActions.setError(error.message));
    } finally {
      dispatch(attendanceActions.setLoading(false));
    }
  }
};

export const getAttendanceSubjects = (id: string): AsyncThunkType => {
  return async dispatch => {
    dispatch(attendanceActions.setLoading(true));
    try {
      const response = await API.attendance.subjects(id);

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
