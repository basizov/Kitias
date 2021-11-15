export type ServerErrorType<T = string> = {
  statusCode: number;
  message: T;
};
