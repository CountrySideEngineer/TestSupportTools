#include <stdio.h>

/*
 *	Stub of subFuncA_002.
 */
//Declare buffers to store a value passed and pass.
int subFuncA_002_called_count;

//Declare buffer to store a value the stub method should return.
int subFuncA_002_return_value[STUB_BUFFER_SIZE_1];

//Declare buffer to store a value passed via arguments.
int subFuncA_002_subInput1[STUB_BUFFER_SIZE_1];

//Declare buffer to store values the stub should return via argument, pointer.
//subInput1 is not output.

//Initialize buffers.
void subFuncA_002_init()
{
	subFuncA_002_called_count = 0; 
	for (int index = 0; index < STUB_BUFFER_SIZE_1; index++)
	{
		subFuncA_002_return_value[index] = 0;
		subFuncA_002_subInput1[index] = 0;
		for (int index2 = 0; index < STUB_BUFFER_SIZE_2; index2++)
		{
			//subInput1 is not output.
		}
	}
}

//Body of stub function.
int subFuncA_002(int subInput1)
{
	int latchReturn = subFuncA_002_return_value[subFuncA_002_called_count];

	//Set argument into buffer.
	subFuncA_002_subInput1[subFuncA_002_called_count] = subInput1;

	//Set back buffer value into argument.
	//subInput1 is not output.

	//Increment called count;
	subFuncA_002_called_count++;

	return latchReturn;
}

