#include <stdio.h>
#include "UserStubHeader.h"
/*
 *	Stub of subFuncA.
 */
//Declare buffers to store a value passed and pass.
int subFuncA_called_count;

//Declare buffer to store a value the stub method should return.
int subFuncA_return_value[STUB_BUFFER_SIZE_1];

//Declare buffer to store a value passed via arguments.
int* subFuncA_subInput1[STUB_BUFFER_SIZE_1];

//Declare buffer to store values the stub should return via argument, pointer.
int subFuncA_subInput1_value[STUB_BUFFER_SIZE_1][STUB_BUFFER_SIZE_2];

//Initialize buffers.
void subFuncA_init()
{
	subFuncA_called_count = 0; 
	for (int index = 0; index < STUB_BUFFER_SIZE_1; index++)
	{
		subFuncA_return_value[index] = 0;
		subFuncA_subInput1[index] = 0;
		for (int index2 = 0; index < STUB_BUFFER_SIZE_2; index2++)
		{
			subFuncA_subInput1_value[index][index2] = 0;
		}
	}
}

//Body of stub function.
int subFuncA(int* subInput1)
{
	int latchReturn = subFuncA_return_value[subFuncA_called_count];

	//Set argument into buffer.
	subFuncA_subInput1[subFuncA_called_count] = subInput1;

	//Set back buffer value into argument.
	subInput1[0] = subFuncA_subInput1_value[subFuncA_called_count][0];

	//Increment called count;
	subFuncA_called_count++;

	return latchReturn;
}

