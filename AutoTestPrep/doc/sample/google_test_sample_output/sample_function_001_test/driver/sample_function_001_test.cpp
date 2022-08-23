#include <stdio.h>
#include <windows.h>
#include "UserHeader.h"

long outsideVariable;
extern short insideVariable;

void sample_function_001_utest::SetUp()
{
	subFuncA_init();
}


TEST_F(sample_function_001_utest, sample_function_001_utest_2)
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	int returnValue = sample_function_001(input1, &input2);

	ASSERT_EQ(2, ret_val);
}

