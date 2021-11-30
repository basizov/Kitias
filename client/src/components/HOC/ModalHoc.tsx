import React from 'react';
import {Modal, Paper, styled} from "@mui/material";

const StyledPaper = styled(Paper)({
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  padding: '1rem'
});

type PropsType = {
  open: boolean;
  onClose: (payload: boolean) => void;
};

export const ModalHoc: React.FC<PropsType> = ({
                                                open,
                                                onClose,
                                                children
                                              }) => {
  return (
    <Modal
      open={open}
      onClose={onClose}
    ><StyledPaper>{children}</StyledPaper></Modal>
  );
};
