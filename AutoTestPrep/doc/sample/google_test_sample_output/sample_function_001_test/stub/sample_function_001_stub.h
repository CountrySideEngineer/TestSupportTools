#pragma once
//Buffer size macro
#ifndef	STUB_BUFFER_SIZE_1
#define	STUB_BUFFER_SIZE_1			(200)
#endif
#ifndef	STUB_BUFFER_SIZE_2
#define	STUB_BUFFER_SIZE_2			(300)
#endif

/*
 *	Buffer for stub of subFuncA.
 */
extern int subFuncA_called_count;

//External declaration of the buffer to store value the method should return.
extern int subFuncA_return_value[];

//External declaration of the buffers which store the value of arguments. 
extern int* subFuncA_subInput1[];

//External declaration of the buffers that store the values the method should return via a pointer argument.
extern int subFuncA_subInput1_value[][STUB_BUFFER_SIZE_2];

//Function to initialize buffers.
void subFuncA_init();

