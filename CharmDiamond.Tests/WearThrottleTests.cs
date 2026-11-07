using CharmDiamond.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CharmDiamond.Tests;

[TestClass]
public class WearThrottleTests
{
	[TestMethod]
	public void FirstHit_IsCharged()
	{
		Assert.IsTrue(new WearThrottle().TryCharge(1, 10f, 0.5f));
	}

	[TestMethod]
	public void HitsInsideTheCooldown_AreFree()
	{
		WearThrottle throttle = new WearThrottle();
		Assert.IsTrue(throttle.TryCharge(1, 10f, 0.5f));
		Assert.IsFalse(throttle.TryCharge(1, 10.2f, 0.5f));
		Assert.IsFalse(throttle.TryCharge(1, 10.49f, 0.5f));
	}

	[TestMethod]
	public void HitAfterTheCooldown_IsChargedAgain()
	{
		WearThrottle throttle = new WearThrottle();
		Assert.IsTrue(throttle.TryCharge(1, 10f, 0.5f));
		Assert.IsTrue(throttle.TryCharge(1, 10.5f, 0.5f));
	}

	[TestMethod]
	public void CooldownIsPerObject()
	{
		WearThrottle throttle = new WearThrottle();
		Assert.IsTrue(throttle.TryCharge(1, 10f, 0.5f));
		Assert.IsTrue(throttle.TryCharge(2, 10.1f, 0.5f));
	}

	[TestMethod]
	public void ZeroCooldown_ChargesEveryHit()
	{
		WearThrottle throttle = new WearThrottle();
		Assert.IsTrue(throttle.TryCharge(1, 10f, 0f));
		Assert.IsTrue(throttle.TryCharge(1, 10f, 0f));
	}

	[TestMethod]
	public void ClockGoingBackwards_ChargesAgain()
	{
		WearThrottle throttle = new WearThrottle();
		Assert.IsTrue(throttle.TryCharge(1, 100f, 0.5f));
		Assert.IsTrue(throttle.TryCharge(1, 0f, 0.5f));
	}

	[TestMethod]
	public void Clear_ForgetsRecords()
	{
		WearThrottle throttle = new WearThrottle();
		Assert.IsTrue(throttle.TryCharge(1, 10f, 0.5f));
		throttle.Clear();
		Assert.IsTrue(throttle.TryCharge(1, 10.1f, 0.5f));
	}
}
