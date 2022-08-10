#include <stdio.h>
#include <windows.h>
#include "UserHeader.h"

//No global variables are refered by function sample_function_002.

//Test target function declare.
int sample_function_002(int input1, int input2);

//Initialize test stub buffers.
void sample_function_002_utest_SetUp()
{
	subFuncA_002_init();
}


static char* sample_function_002_utest_1()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int returnValue = sample_function_002(input1, input2);

	mu_assert(1 == ret_val);
}

static char* sample_function_002_utest_2()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int returnValue = sample_function_002(input1, input2);

	mu_assert(2 == ret_val);
}

char* sample_function_002_utest_run_all()
{
	mu_run_test("sample_function_002_utest_1", sample_function_002_utest_1);
	mu_run_test("sample_function_002_utest_2", sample_function_002_utest_2);

	return 0;
}

